# Xamarin iOS PayPal binding library

This is a Xamarin.iOS binding library for Paypal SDK.   
Last version is available on NuGet: [Xam.Paypal.iOS](https://www.nuget.org/packages/Xam.PayPal.iOS/)

Please see [PayPalSdk for iOS onGitHub](https://github.com/paypal/PayPal-iOS-SDK) for SDK documentation.


## Usage

 In your controller setup your payment:

		private void SetupPayPal()
		{
			// setup your delegate
			this._myDelegate = new LurchPayPalDelegate()
				.OnCancelDo((controller) =>
				{
					// do something on payment cancel

					controller?.DismissViewController(true, null);
				})
				.OnCompleteDo((controller, payPalPayment) =>
				{
					// do something on payment complete

					controller?.DismissViewController(true, () => { });
				});

			// init with clientid
			PayPalMobile.InitializeWithClientIdsForEnvironments(NSDictionary.FromObjectsAndKeys(
				new NSObject[] {
					new NSString ("SANDBOXCLIENTID"),
					new NSString ("PRODUCTIONCLIENTID"),
				}, new NSObject[] {
					Constants.PayPalEnvironmentSandbox
					Constants.PayPalEnvironmentProduction,
				}
			));

			// set up your enviroment
			PayPalMobile.PreconnectWithEnvironment(Constants.PayPalEnvironmentSandbox.ToString());
			//PayPalMobile.PreconnectWithEnvironment(Constants.PayPalEnvironmentProduction.ToString());

			this._payPalConfig = new PayPalConfiguration();
			this._payPalConfig.AcceptCreditCards = true;

			this._payPalConfig.MerchantName = "MARKJACKMILIAN";
			this._payPalConfig.MerchantPrivacyPolicyURL = new NSUrl("http://www.markjackmilian.net");
			this._payPalConfig.MerchantUserAgreementURL = new NSUrl("http://www.markjackmilian.net");
			this._payPalConfig.LanguageOrLocale = NSLocale.PreferredLanguages[0];
			this._payPalConfig.PayPalShippingAddressOption = PayPalShippingAddressOption.Provided;

		}

Now you can go with payment:

	public void Pay()
	{
	    var payment = PayPalPayment.PaymentWithAmount(new NSDecimalNumber(24.12), "EUR", "Example", PayPalPaymentIntent.Sale);
	    var paymentViewController = new PayPalPaymentViewController(payment, _payPalConfig, this._myDelegate);
	    this.PresentViewController(paymentViewController, true, null);
	}



##Note about versioning##
Master branch contains the last published version on Nuget and has every previous version tagged.
The version is based on PayPal sdk version:

 - First three number are PayPalSdk version.
 - Fourth number is for binding library revision.

 

Have fun!

##Follow Me

 - Twitter: [@markjackmilian](https://twitter.com/markjackmilian)
 - MyBlog: [markjackmilian.net](http://markjackmilian.net/blog)
 - Linkedin: [linkedin](https://www.linkedin.com/in/marco-giacomo-milani)