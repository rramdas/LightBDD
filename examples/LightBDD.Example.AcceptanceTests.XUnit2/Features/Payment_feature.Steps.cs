using System.Threading.Tasks;
using LightBDD.Example.Helpers;
using LightBDD.XUnit2;
using Xunit.Abstractions;

namespace LightBDD.Example.AcceptanceTests.XUnit2.Features
{
    public partial class Payment_feature : FeatureFixture
    {
        #region Setup/Teardown
        public Payment_feature(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion
        private async Task Given_customer_has_some_products_in_basket()
        {
            await LongRunningOperationSimulator.SimulateAsync();
        }

        private async Task Given_customer_has_enough_money_to_pay_for_products()
        {
            await LongRunningOperationSimulator.SimulateAsync();
        }

        private async Task When_customer_requests_to_pay()
        {
            await LongRunningOperationSimulator.SimulateAsync();
        }

        private async Task Then_payment_should_be_successful()
        {
            await LongRunningOperationSimulator.SimulateAsync();
        }
    }
}