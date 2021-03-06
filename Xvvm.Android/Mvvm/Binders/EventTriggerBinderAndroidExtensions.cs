using Android.App;
using Android.Content;
using System;
using Xvvm.Mvvm;
using Xvvm.Mvvm.Binders;
using Xvvm.Mvvm.Bindings;
using Xvvm.Utils;

namespace Xvvm.Android.Mvvm.Binders
{
    public static class EventTriggerBinderAndroidExtensions
    {
        public static EventTriggerBinder<T> BindToAlertDialogMessage<T>(this EventTriggerBinder<T> binder,
            Context context, string title, IValueConverter<T, string> valueConverter = null)
        {
            var weakContext = new WeakReference<Context>(context);
            binder.Bindings.Add(new EventHandlerBinding<T>(binder.AddDelegate, binder.RemoveDelegate, (sender, args) =>
            {
                var context_ = weakContext.Get();
                if (context_ != null)
                {
                    new AlertDialog.Builder(context_)
                        .SetTitle(title)
                        .SetMessage(valueConverter.GetStringValue(args))
                        .SetPositiveButton(global::Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
                        .Create()
                        .Show();
                }
            }));
            return binder;
        }

        public static EventTriggerBinder<T> BindToAlertDialogMessage<T>(this EventTriggerBinder<T> binder,
            Context context, int titleResourceId, IValueConverter<T, string> valueConverter = null)
        {
            return binder.BindToAlertDialogMessage(context, context.GetString(titleResourceId), valueConverter);
        }
    }
}
