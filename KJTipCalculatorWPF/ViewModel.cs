using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KJTipCalculatorWPF.Annotations;

namespace KJTipCalculatorWPF
{
    /// <summary>
    /// Tip calculator view model
    /// </summary>
    internal class ViewModel: INotifyPropertyChanged
    {
        /// <summary>
        /// Simple implementation of ICommand for commands that can always be executed.
        /// </summary>
        internal class ActionCommand : ICommand
        {
            private readonly Action _action;

            public ActionCommand(Action action)
            {
                _action = action;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }

        public ICommand ClearSubtotalCommand { get; private set; }
        public ICommand IncrementTipPercentageCommand { get; private set; }
        public ICommand DecrementTipPercentageCommand { get; private set; }
        public ICommand IncrementNumberInPartyCommand { get; private set; }
        public ICommand DecrementNumberInPartyCommand { get; private set; }

        public ViewModel()
        {
            ClearSubtotalCommand = new ActionCommand(this.ClearSubtotal);
            IncrementTipPercentageCommand = new ActionCommand(this.IncrementTipPercentage);
            DecrementTipPercentageCommand = new ActionCommand(this.DecrementTipPercentage);
            IncrementNumberInPartyCommand = new ActionCommand(this.IncrementNumberInParty);
            DecrementNumberInPartyCommand = new ActionCommand(this.DecrementNumberInParty);
        }

        public decimal Subtotal
        {
            get { return _calc.Subtotal; }

            set
            {
                if (value == _calc.Subtotal) return;
                _calc.Subtotal = value;
                OnPropertyChanged(nameof(Subtotal));
                OnCalculatedPropertiesChanged();
            }
        }

        public int TipPercentage
        {
            get { return _calc.TipPercentage; }

            set
            {
                if (value == _calc.TipPercentage) return;
                _calc.TipPercentage = value;
                OnPropertyChanged(nameof(TipPercentage));
                OnCalculatedPropertiesChanged();
            }
        }

        public int NumberInParty
        {
            get { return _calc.NumberInParty; }

            set
            {
                if (value == _calc.NumberInParty) return;
                _calc.NumberInParty = value;
                OnPropertyChanged(nameof(NumberInParty));
                OnCalculatedPropertiesChanged();
            }
        }

        public decimal Tip => _calc.Tip;

        public decimal Total => _calc.Total;

        public decimal PerPerson => _calc.PerPerson;

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The primitive tip-calculator model encapsulated by this view model.
        /// </summary>
        private readonly TipCalculation _calc = new TipCalculation();

        private void ClearSubtotal()
        {
            Subtotal = 0;
        }

        private void IncrementTipPercentage()
        {
            TipPercentage += 1;
        }

        private void DecrementTipPercentage()
        {
            if (TipPercentage > 1)
                TipPercentage -= 1;
            else
                TipPercentage = 1;
        }

        private void IncrementNumberInParty()
        {
            NumberInParty += 1;
        }

        private void DecrementNumberInParty()
        {
            if (NumberInParty > 1)
                NumberInParty -= 1;
            else
                NumberInParty = 1;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Call OnPropertyChanged() for all calculated values.
        /// </summary>
        /// <remarks>
        /// This method must be called whenever any of the calculator input values change.
        /// </remarks>
        private void OnCalculatedPropertiesChanged()
        {
            OnPropertyChanged(nameof(Tip));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(PerPerson));
        }
    }
}
