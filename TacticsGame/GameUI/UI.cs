using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;
using TacticsGame.Cards;
using TacticsGame.Converters;
using TacticsGame.Core.Dto;

namespace TacticsGame.GameUI
{
    public class UI
    {
        private StackPanel _queuePanel;
        private ObservableCollection<int> _units;
        //private ObservableCollection<UnitDto> _units;
        private Button _weaponButton;
        private Button _passButton;
        private int _round = 1;
        private int _info = 0;
        private UnitCard _unitCard;
        private RoundCard _roundImagePath = new(@$"{Directory.GetCurrentDirectory()}\TacticsGame\Assets\Icons\UI\Round.png");
        private static string _path = @$"{Directory.GetCurrentDirectory()}\TacticsGame\Assets\Icons\Units\";
        private Dictionary<int, UnitCard> _unitsCards = new();
        private DtoProvider _dtoProvider;
        public UI(StackPanel queuePanel, ObservableCollection<int> units, Button weaponButton, Button passButton, DtoProvider dtoProvider)
        {
            _queuePanel = queuePanel;
            _units = units;
            _weaponButton = weaponButton;
            _passButton = passButton;
            _dtoProvider = dtoProvider;
            SetBindings();
            _passButton.Click += PassButton_Click;
            _units.CollectionChanged += ChangeCollection;
        }
        public void SetBindings()
        {
            _weaponButton.SetBinding(Button.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.1 });
            _weaponButton.SetBinding(Button.HeightProperty, new Binding("ActualHeight") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
            //gunButton.SetBinding(Button.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
            //gunButton.SetBinding(Button.HeightProperty, new Binding("ActualHeight") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
            _passButton.SetBinding(Button.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
            _passButton.SetBinding(Button.HeightProperty, new Binding("ActualHeight") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.05 });
            _queuePanel.SetBinding(StackPanel.WidthProperty, new Binding("ActualWidth") { Source = this, Converter = new PercentConverter(), ConverterParameter = 0.5 });
        }
        private void ChangeCollection(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddUnitCard();
                    _queuePanel.Children.Clear();
                    _round = 1;
                    _info = 0;
                    UpdateQueue();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removedUnit = (int)e.OldItems[0];
                    var panelChildren = _queuePanel.Children;
                    for (var i = 0; i < panelChildren.Count; i++)
                    {
                        var child = (Border)_queuePanel.Children[i];
                        if (child.Name != "Unit" + removedUnit.ToString())
                        {
                            continue;
                        }
                        _queuePanel.Children.Remove(child); // удаляем найденный элемент
                        i--; // уменьшаем индекс, чтобы не пропустить следующий элемент
                    }                   
                    _unitsCards.Remove(removedUnit);
                    UpdateQueue();
                    break;
                case NotifyCollectionChangedAction.Move:

                    break;
            }
        }
        private void AddUnitCard()
        {
            var unitId = _units[^1];
            var unitCard = new UnitCard(_dtoProvider.CreateUnitDto(unitId));
            _unitsCards.Add(unitId, unitCard);
        }
        private void UpdateQueue()
        {
            while (_queuePanel.Children.Count < 10)
            {
                if (_round != 1 && _info == _units.Count || _info > _units.Count)
                {
                    var roundCard = _roundImagePath.CreateRoundBorder(_round);
                    _queuePanel.Children.Add(roundCard);
                    _info = 0;
                }
                foreach (var unitCard in _unitsCards)
                {
                    var border = unitCard.Value.CreateUnitBorder();
                    if(_queuePanel.Children.Count >= 10) break;
                    _queuePanel.Children.Add(border);

                    _info++;

                }
                if (_info == _units.Count)
                {
                    _round++;
                }

            }
        }
        private void Remove_Card(object sender, RoutedEventArgs e)
        {
            _queuePanel.Children.Remove((Border)sender);
            var firstCard = (Border)_queuePanel.Children[0];
            var secondCard = (Border)_queuePanel.Children[1];
            if (((Grid)firstCard.Child).Children[1] is TextBlock && ((Grid)secondCard.Child).Children[1] is TextBlock)
            {
                _queuePanel.Children.Remove(firstCard);
            }
        }
        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            _queuePanel.Children.RemoveAt(0);
            var firstCard = (Border)_queuePanel.Children[0];
            var secondCard = (Border)_queuePanel.Children[1];
            if (((Grid)firstCard.Child).Children[1] is TextBlock || ((Grid)firstCard.Child).Children[1] is TextBlock && ((Grid)secondCard.Child).Children[1] is TextBlock)
            {
                _queuePanel.Children.Remove(firstCard);
            }
            UpdateQueue();
        }
    }
}
