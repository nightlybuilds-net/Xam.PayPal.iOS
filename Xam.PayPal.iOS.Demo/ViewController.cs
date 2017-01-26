using System;
using Foundation;
using UIKit;

namespace Xam.PayPal.iOS.Demo
{
	public partial class ViewController : UIViewController
	{
		// USE YOUR CLIENT ID HERE
		private const string CLIENTID = "AYaP1kdIcz2maWEmebyohxNCMYkCCsLDsYH5xcufnr4fv4cw_uAKITOhtYPPqXpM7ASGlcpPTXBsDDX2";
		private PayPalConfiguration _payPalConfig;
		private MyPaymentDelegate _myDelegate;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		partial void PayNowTouched(Foundation.NSObject sender)
		{
			var payment = PayPalPayment.PaymentWithAmount(new Foundation.NSDecimalNumber(24.12), "EUR", "Mark Jack Milian", PayPalPaymentIntent.Sale);

			var paymentViewController = new PayPalPaymentViewController(payment, _payPalConfig, this._myDelegate);
			this.PresentViewController(paymentViewController, true, null);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// setup your delegate
			this._myDelegate = new MyPaymentDelegate()
				.OnCancelDo((controller) => 
				{
					this.ResultLbl.Hidden = false;
					this.ResultLbl.Text = "On Cancel Invoked";
					controller?.DismissViewController(true, null);
				})
				.OnCompleteDo((controller, payPalPayment) => 
				{
					this.ResultLbl.Hidden = false;
					this.ResultLbl.Text = "On complete Invoked";
					controller?.DismissViewController(true, () =>
					{
						// in paypalPayment you have info about payment
						
					});
				});

			// init with clientid
			PayPalMobile.InitializeWithClientIdsForEnvironments(NSDictionary.FromObjectsAndKeys(
				new NSObject[] {
					new NSString (CLIENTID),
				}, new NSObject[] {
					//Constants.PayPalEnvironmentProduction,
					Constants.PayPalEnvironmentSandbox
				}
			));

			// set up your enviroment
			PayPalMobile.PreconnectWithEnvironment(Constants.PayPalEnvironmentSandbox.ToString());
			//PayPalMobile.PreconnectWithEnvironment(Constants.PayPalEnvironmentProduction.ToString());

			this._payPalConfig = new PayPalConfiguration();
			this._payPalConfig.AcceptCreditCards = true;

			_payPalConfig.MerchantName = "Mark Jack Milian";
			_payPalConfig.MerchantPrivacyPolicyURL = new NSUrl("http://www.markjackmilian.net");
			_payPalConfig.MerchantUserAgreementURL = new NSUrl("http://www.markjackmilian.net");
			_payPalConfig.LanguageOrLocale = NSLocale.PreferredLanguages[0];
			_payPalConfig.PayPalShippingAddressOption = PayPalShippingAddressOption.PayPal;

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


	}

	
}
