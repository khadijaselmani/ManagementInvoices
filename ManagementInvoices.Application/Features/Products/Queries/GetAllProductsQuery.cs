using ManagementInvoices.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementInvoices.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}
