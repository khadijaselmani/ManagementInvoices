using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementInvoices.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public UpdateProductCommand(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
