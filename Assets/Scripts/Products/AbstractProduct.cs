namespace Shop
{
    public abstract class AbstractProduct
    {
        protected string _internalName;
        protected CurrencyType _currencyType;
        protected int _cost, _maxCount, _availableCount;

        protected CurrencyType Type
        {
            get => _currencyType;
            set => _currencyType = value;
        }

        protected int MaxCount
        {
            get => _maxCount;
            set => _maxCount = value;
        }

        protected int Cost
        {
            get => _cost;
            set => _cost = value;
        }

        protected int AvailableCount
        {
            get => _availableCount;
            set => _availableCount = value;
        }

        public string InternalName => _internalName;
    }
}