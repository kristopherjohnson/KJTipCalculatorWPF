using System.ComponentModel;
using System.Runtime.CompilerServices;
using KJTipCalculatorWPF.Annotations;

namespace KJTipCalculatorWPF
{
    class ViewModel: INotifyPropertyChanged
    {
        private readonly TipCalculation _calc = new TipCalculation();

        public decimal Subtotal
        {
            get { return _calc.Subtotal; }
            set
            {
                if (value == _calc.Subtotal) return;
                _calc.Subtotal = value;
                OnPropertyChanged(nameof(Subtotal));
                OnCalcPropertyChanged();
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
                OnCalcPropertyChanged();
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
                OnCalcPropertyChanged();
            }
        }

        public decimal Tip
        {
            get { return _calc.Tip; }
        }

        public decimal Total
        {
            get { return _calc.Total; }
        }

        public decimal PerPerson
        {
            get { return _calc.PerPerson; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCalcPropertyChanged()
        {
            OnPropertyChanged(nameof(Tip));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(PerPerson));
        }
    }
}
