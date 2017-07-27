namespace Moo2U.Behaviors {
    using System;
    using Xamarin.Forms;

    public abstract class BehaviorBase<T> : Behavior<T> where T : BindableObject {

        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindable) {
            base.OnAttachedTo(bindable);
            this.AssociatedObject = bindable;

            if (bindable.BindingContext != null) {
                this.BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        void OnBindingContextChanged(Object sender, EventArgs e) {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }

        protected override void OnDetachingFrom(T bindable) {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            this.AssociatedObject = null;
        }

    }
}
