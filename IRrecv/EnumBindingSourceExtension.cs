using System;
using System.Windows.Markup;

namespace IRrecv
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        #region Properties

        public Type EnumType
        {
            get => m_EnumType;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("The EnumType must be specified.", nameof(EnumType));
                if (value != m_EnumType)
                {
                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                    if (!enumType.IsEnum)
                        throw new ArgumentException("Type must be an Enum.", nameof(EnumType));
                    m_EnumType = value;
                }
            }
        }
        private Type m_EnumType;

        #endregion

        #region Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Type enumType = Nullable.GetUnderlyingType(m_EnumType) ?? m_EnumType;
            return Enum.GetValues(enumType);
        }

        #endregion

        #region Constructor

        public EnumBindingSourceExtension()
        { }

        public EnumBindingSourceExtension(Type enumType) => EnumType = enumType;

        #endregion
    }
}
