public partial class SpinnerButton : UIButton
    {
        public SpinnerButton (IntPtr handle) : base (handle)
        {
          // Sets the default styling of the button. 
            SetStyling();
            
            // We weant the title to be invinsible in the selected state
            SetTitleColor(UIColor.Clear, UIControlState.Selected);
            // the tint color needs to be clear. Otherwise when Selected == true, we will see the default tintcolor.
            TintColor = UIColor.Clear;
        }

        UIActivityIndicatorView _spinner;

        // Don't override this method unless you also need to update some UI when the title text changes.
        public override void SetTitle(string title, UIControlState forState)
        {
            base.SetTitle(title, forState);
            Superview.SetNeedsLayout();
            Layer.CornerRadius = Layer.Frame.Height / 2;
        }

        public void ShowSpinner(UIView parentView, UIActivityIndicatorViewStyle style)
        {
            // We don't want to add the spinner to the button. We  want to add it to another parentView 
            // and then just center the spinner on top of the button here.
            _spinner = AddSpinnerToView(parentView, style);
            CenterView(_spinner, this);
            
            // When we change Selected to true, the title color will change to clear. See line 9.
            Selected = true;
            _spinner.StartAnimating();
        }

        public void HideSpinner()
        {
            _spinner.RemoveFromSuperview();
            Selected = false;
        }
        
        // Change content of this method to your specific design
        void SetStyling() 
        {
            Font = Font(FontType.FontSemiBold, 18f, 24f);
            SetTitleColor(UIColor.White, UIControlState.Normal);
            BackgroundColor = UIColor.Clear;
            TitleLabel.AdjustsFontSizeToFitWidth = true;
            Layer.BorderWidth = 1;
            Layer.BorderColor = UIColor.White.CGColor;
            Layer.CornerRadius = Layer.Frame.Height / 2;
        }
        
        public static UIActivityIndicatorView AddSpinnerToView(UIView parentView, UIActivityIndicatorViewStyle style)
        {
            UIActivityIndicatorView spinner = new UIActivityIndicatorView(style);
            spinner.HidesWhenStopped = true;
            spinner.TranslatesAutoresizingMaskIntoConstraints = false;

            parentView.Add(spinner);

            return spinner;
        }
        
        public static void CenterView(UIView viewToCenter, UIView referenceView)
        {
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] {
                viewToCenter.CenterXAnchor.ConstraintEqualTo(referenceView.CenterXAnchor),
                viewToCenter.CenterYAnchor.ConstraintEqualTo(referenceView.CenterYAnchor)
            });
        }
        
    }
