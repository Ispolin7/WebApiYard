using System;
using System.Collections.Generic;
using System.Linq;
using WebApiYard.Common;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Models;

namespace WebApiYard.Mappings
{
    public class RepositoryToServiceMapper
    {
        /// <summary>
        /// Mapping customer repository model into service
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>service instance/returns>
        public CustomerServiceModel MapCustomer(Customer customer)
        {
            if(customer == null)
            {
                return new CustomerServiceModel();
            }

            return new CustomerServiceModel
            {
                Id = customer.Id,
                Age = customer.Age,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Orders = this.MapOrders(customer.Orders)
            };
        }

        /// <summary>
        /// Mapping order repository model into service
        /// </summary>
        /// <param name="order"></param>
        /// <returns>service instance</returns>
        public OrderServiceModel MapOrder(Order order)
        {
            if(order == null)
            {
                return new OrderServiceModel();
            }

            return new OrderServiceModel
            {
                Id = order.Id,
                AddressId = order.AddressId,
                ShippingAddress = this.MapAddress(order.ShippingAddress),
                CustomerId = order.CustomerId,
                OrderDate = order.CreatedAT,
                Items = this.MapOrderItems(order.Items)
            };
        }

        /// <summary>
        /// Mapping address repository model into service
        /// </summary>
        /// <param name="address"></param>
        /// <returns>service instance</returns>
        public AddressServiceModel MapAddress(Address address)
        {
            if (address == null)
            {
                return new AddressServiceModel();
            }

            return new AddressServiceModel
            {
                Id = address.Id,
                StreetLine1 = address.StreetLine1,
                StreetLine2 = address.StreetLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country
            };
        }

        /// <summary>
        /// Mapping order item repository model into service
        /// </summary>
        /// <param name="item"></param>
        /// <returns>service instance</returns>
        public OrderItemServiceModel MapOrderItem(OrderItem item)
        {
            if (item == null)
            {
                return new OrderItemServiceModel();
            }

            return new OrderItemServiceModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                PurchasePrice = item.PurchasePrice,
                Color = Enum.GetName(typeof(Colors), item.Color),
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Product = this.MapProduct(item.Product)
            };
        }

        /// <summary>
        /// Mapping product repository model into service
        /// </summary>
        /// <param name="product"></param>
        /// <returns>service instance</returns>
        public ProductServiceModel MapProduct(Product product)
        {
            if(product == null)
            {
                return new ProductServiceModel();
            }

            return new ProductServiceModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        /// <summary>
        /// Mapping customer repository list into service
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>list</returns>
        public IEnumerable<CustomerServiceModel> MapCustomers(IEnumerable<Customer> customers)
        {
            if (customers == null || !customers.Any())
            {
                return new List<CustomerServiceModel>();               
            }

            return customers.Select(customer => this.MapCustomer(customer));
        }

        /// <summary>
        /// Mapping order repository list into service
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>list</returns>
        public IEnumerable<OrderServiceModel> MapOrders(IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any())
            {
                return new List<OrderServiceModel>();
            }

            return orders.Select(order => this.MapOrder(order));
        }

        /// <summary>
        /// Mapping order item repository list into service
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns>list</returns>
        public IEnumerable<OrderItemServiceModel> MapOrderItems(IEnumerable<OrderItem> orderItems)
        {
            if (orderItems == null || !orderItems.Any())
            {
                return new List<OrderItemServiceModel>();
            }

            return orderItems.Select(orderItem => this.MapOrderItem(orderItem));
        }

        /// <summary>
        /// Mapping product repository list into service
        /// </summary>
        /// <param name="products"></param>
        /// <returns>list</returns>
        public IEnumerable<ProductServiceModel> MapProducts(IEnumerable<Product> products)
        {
            if (products == null || !products.Any())
            {
                return new List<ProductServiceModel>();
            }

            return products.Select(product => this.MapProduct(product));
        }
    }
}
