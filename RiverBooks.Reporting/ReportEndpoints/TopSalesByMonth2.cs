using FastEndpoints;

namespace RiverBooks.Reporting.ReportEndpoints;

internal class TopSalesByMonth2(ISalesReportService reportService) :
  Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
  public override void Configure()
  {
    Get("/topsales2");
    AllowAnonymous(); // TODO: lock down
  }

  public override async Task HandleAsync(
  TopSalesByMonthRequest request,
  CancellationToken ct = default)
  {
    var report = await reportService.GetTopBooksByMonthReportAsync(
      request.Month, request.Year);
    var response = new TopSalesByMonthResponse { Report = report };
    await SendAsync(response, cancellation: ct);
  }

}
