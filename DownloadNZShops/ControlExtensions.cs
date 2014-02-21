using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace DownloadNZShops
{
    public static class ControlExtensions
    {
        /// <summary>
        /// 多线程更新UI属性值,界面不卡
        /// 调用：myButton.SetPropertyInGuiThread(c => c.Text, "Click Me!")
        /// 来源：http://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="control"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void SetPropertyThreadSafe<C, V>(this C control, Expression<Func<C, V>> property, V value) where C : Control
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("The 'property' expression must specify a property on this control.");

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("The 'property' expression must specify a property on this control.");

            if (control.InvokeRequired)
                control.Invoke(
                    (Action<C, Expression<Func<C, V>>, V>)SetPropertyThreadSafe,
                    new object[] { control, property, value }
                );
            else
                propertyInfo.SetValue(control, value, null);
        }
    }
}
