using System;
using System.Reflection;
using Mail.Mdoel;

namespace Mail
{
    public class RegisterMail : MailBase
    {
        private readonly Register register;

        public RegisterMail(Register register)
        {
            this.register = register;
        }

        public override Type TemplateType => typeof(Register);

        public override object GetValue(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetValue(register, null);
        }
    }
}
