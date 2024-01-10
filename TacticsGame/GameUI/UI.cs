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
using System.Windows.Documents;
using System.Windows.Input;
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
        private UnitCard _currentCard;
        private int _currentCardIndex;
        public UI(StackPanel queuePanel, ObservableCollection<int> units, Button weaponButton, Button passButton, DtoProvider dtoProvider)
        {
            _queuePanel = queuePanel;
            _units = units;
            _weaponButton = weaponButton;
            _passButton = passButton;
            _dtoProvider = dtoProvider;
            SetBindings();
            _passButton.Click += PassButton_Click;
            //_weaponButton.Click += WeaponButton_Click;
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
                    //_currentCard = _unitsCards.Last().Value;
                    //_currentCardIndex = _unitsCards.Last().Key;
                    if (_queuePanel.Children.Count > 0)
                    {
                        var lastCard = (Border)_queuePanel.Children[^1];
                        if (((Grid)lastCard.Child).Children[1] is TextBlock)
                        {
                            _queuePanel.Children.Remove(lastCard);
                        }
                    }                    
                    FillQueue();
                    
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

                    //_currentCard = _unitsCards[0];
                    UpdateQueue(_currentCard);
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
        private void UpdateQueue(UnitCard unitCard)
        {
            if (_queuePanel.Children.Count < 10)
            {
                if (_round != 1 && _info == _units.Count || _info > _units.Count)
                {
                    var roundCard = _roundImagePath.CreateRoundBorder(_round);
                    _queuePanel.Children.Add(roundCard);
                    _info = 0;
                }
                var border = unitCard.CreateUnitBorder();
                _queuePanel.Children.Add(border);

                _info++;

                if (_info == _units.Count)
                {
                    _round++;
                }
            }
        }
        private void Update(UnitCard unitCard)
        {            
                var border = unitCard.CreateUnitBorder();
                _queuePanel.Children.Add(border);
        }
        private void FillQueue()
        {
            while (_queuePanel.Children.Count < 10)
            {
                if (_round != 1 && _info == _units.Count || _info > _units.Count)
                {
                    var roundCard = _roundImagePath.CreateRoundBorder(_round);
                    _queuePanel.Children.Add(roundCard);
                    _info = 0;
                }
                foreach(var unitCard in _unitsCards)
                {
                    _currentCard = unitCard.Value;
                    _currentCardIndex = unitCard.Key;
                    _weaponButton.Tag = _currentCard._unit.WeaponsDtos[0].WeaponId;
                    if (_queuePanel.Children.Count == 10)
                    {                        
                        break;
                    }

                    Update(_currentCard);

                    _info++;
                }

                if (_info == _units.Count)
                {
                    _round++;
                }

            }
            _currentCard = _unitsCards.First().Value;
            _currentCardIndex = _unitsCards.First().Key;
            _weaponButton.Tag = _currentCard._unit.WeaponsDtos[0].WeaponId;
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
            int index = _unitsCards.FirstOrDefault(x => x.Value == _currentCard).Key;
            _currentCard = _unitsCards.SkipWhile(x => x.Key != index).Skip(1).FirstOrDefault().Value;
            _weaponButton.Tag = _currentCard._unit.WeaponsDtos[0].WeaponId;
        }
        private void PassButton_Click(object sender, RoutedEventArgs e)
        {            
            int index = _unitsCards.FirstOrDefault(x => x.Value == _currentCard).Key;
            if (index == _unitsCards.Last().Key)
            {
                _currentCard = _unitsCards.First().Value;
                _weaponButton.Tag = _currentCard._unit.WeaponsDtos[0].WeaponId;
            }
            else
            {
                _currentCard = _unitsCards.SkipWhile(x => x.Key != index).Skip(1).FirstOrDefault().Value;
                _weaponButton.Tag = _currentCard._unit.WeaponsDtos[0].WeaponId;
            }
            _queuePanel.Children.RemoveAt(0);
            var firstCard = (Border)_queuePanel.Children[0];
            var secondCard = (Border)_queuePanel.Children[1];
            if (((Grid)firstCard.Child).Children[1] is TextBlock || ((Grid)firstCard.Child).Children[1] is TextBlock && ((Grid)secondCard.Child).Children[1] is TextBlock)
            {
                _queuePanel.Children.Remove(firstCard);

            }
            UpdateQueue(_currentCard);
        }

        //private void WeaponButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var currentWeapon = _currentCard._unit.WeaponsDtos[0];
        //}

    }
}
