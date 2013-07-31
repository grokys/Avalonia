namespace Avalonia
{
    public class UIPropertyMetadata : PropertyMetadata
    {
        public UIPropertyMetadata()
        {
        }

        public UIPropertyMetadata(object defaultValue)
            : base(defaultValue)
        {
        }

        public UIPropertyMetadata(PropertyChangedCallback propertyChangedCallback)
            : base(propertyChangedCallback)
        {
        }


        public UIPropertyMetadata(
            object defaultValue, 
            PropertyChangedCallback propertyChangedCallback)
            : base(defaultValue, propertyChangedCallback)
        {
        }

        public UIPropertyMetadata(
            object defaultValue,
            PropertyChangedCallback propertyChangedCallback,
            CoerceValueCallback coerceValueCallback)
            : base(defaultValue, propertyChangedCallback, coerceValueCallback)
        {
        }

        public UIPropertyMetadata(
            object defaultValue,
            PropertyChangedCallback propertyChangedCallback,
            CoerceValueCallback coerceValueCallback,
            bool isAnimationProhibited)
            : base(defaultValue, propertyChangedCallback, coerceValueCallback)
        {
            this.isAnimationProhibited = isAnimationProhibited;
        }

        public bool IsAnimationProhibited
        {
            get { return isAnimationProhibited; }
            set { isAnimationProhibited = value; }
        }

        bool isAnimationProhibited;
    }
}