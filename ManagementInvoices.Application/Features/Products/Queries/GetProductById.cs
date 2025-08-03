using ManagementInvoices.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementInvoices.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
