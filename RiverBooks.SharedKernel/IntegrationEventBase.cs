using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RiverBooks.SharedKernel;
public abstract class IntegrationEventBase :INotification
{
  public DateTimeOffset DateTimeOffset { get; protected set; } = DateTimeOffset.UtcNow;
}
