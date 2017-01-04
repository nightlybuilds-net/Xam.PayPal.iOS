using System;

using UIKit;

namespace Xam.PayPal.iOS.Demo
{

	class MyPaymentDelegate : PayPalPaymentDelegate
	{

		private Action<PayPalPaymentViewController> _onCancel;
		private Action<PayPalPaymentViewController, PayPalPayment> _onComplete;


		public MyPaymentDelegate OnCompleteDo(Action<PayPalPaymentViewController, PayPalPayment> onComplete)
		{
			this._onComplete = onComplete;
			return this;
		}

		public MyPaymentDelegate OnCancelDo(Action<PayPalPaymentViewController> onCancel)
		{
			this._onCancel = onCancel;
			return this;
		}


		public override void PayPalPaymentDidCancel(PayPalPaymentViewController paymentViewController)
		{
			this._onCancel?.Invoke(paymentViewController);
		}

		public override void PayPalPaymentViewController(PayPalPaymentViewController paymentViewController, PayPalPayment completedPayment)
		{
			this._onComplete?.Invoke(paymentViewController, completedPayment);
		}
	}
}
