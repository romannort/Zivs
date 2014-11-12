using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zivs.SupportUi
{
    public abstract class ViewModelBase: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression,
            Action afterChangedCallBack)
        {
            OnPropertyChanged(propertyExpression);
            if (PropertyChanged != null)
            {
                afterChangedCallBack.Invoke();
            }
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged != null)
            {
                String propertyName = GetPropertyName(propertyExpression);
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected String GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return ((MemberExpression)propertyExpression.Body).Member.Name;
        }
    }
}
