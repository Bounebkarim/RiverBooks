using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace RiverBooks.Reporting.ReportEndpoints;
internal class TopSalesByMonthRequest
{
  [FromQuery]
  public int Month { get; set; }
  [FromQuery]
  public int Year { get; set; }
}
public class TopSalesByMonthResponse
{
  public TopBooksByMonthReport Report { get; set; } = default!;
}

internal class TopSalesByMonth(ITopSellingBooksReportService reportService) :
  Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
  public override void Configure()
  {
    Get("/topsales");
    AllowAnonymous(); // TODO: lock down
  }

  public override async Task HandleAsync(
  TopSalesByMonthRequest request,
  CancellationToken ct = default)
  {
    var report = reportService.ReachInSqlQuery(request.Month, request.Year);
    var response = new TopSalesByMonthResponse { Report = report };
    await SendAsync(response, cancellation: ct);
  }

}



