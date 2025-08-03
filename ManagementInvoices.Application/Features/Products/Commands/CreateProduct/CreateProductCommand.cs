﻿using ManagementInvoices.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementInvoices.Application.Features.Products.Commands.CreateProduct
{
    // CreateProductCommand.cs
    public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

   

}
