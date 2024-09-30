using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class CustomerMappers
    {
        public static CustomerDto ToCustomerDto(this Customer customerModel)
        {
            return new CustomerDto
            {
                CustomerId = customerModel.CustomerId,
                Name = customerModel.Name,
                Email = customerModel.Email,
                Phone = customerModel.Phone,
                Address = customerModel.Address,
                ImageUrl = customerModel.ImageUrl,
                Password = customerModel.Password,
            };
        }

        public static CustomerDto ToCustomerDtoWithOrders(this Customer customerModel)
        {
            return new CustomerDto
            {
                CustomerId = customerModel.CustomerId,
                Name = customerModel.Name,
                Email = customerModel.Email,
                Phone = customerModel.Phone,
                Address = customerModel.Address,
                ImageUrl = customerModel.ImageUrl,
                Password = customerModel.Password,
                Orders = customerModel.Orders.Select(order => order.ToOrderDto()).ToList()
            };
        }

        public static CustomerDto ToCustomerDtoWithPreviews(this Customer customerModel)
        {
            return new CustomerDto
            {
                CustomerId = customerModel.CustomerId,
                Name = customerModel.Name,
                Email = customerModel.Email,
                Phone = customerModel.Phone,
                Address = customerModel.Address,
                ImageUrl = customerModel.ImageUrl,
                Password = customerModel.Password,
                ProductReviews = customerModel.ProductReviews.Select(review => review.ToProductReviewDto()).ToList()
            };
        }

        public static Customer ToCustomerFromCreateDto(this CreateCustomerDto customerDto)
        {
            return new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
                ImageUrl = customerDto.ImageUrl,
                Password = customerDto.Password,
            };
        }
    }
}