using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Common;
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
        public Customer MapCustomer(Repositories.Models.Customer customer)
        {
            if(customer == null)
            {
                return new Customer();
            }

            return new Customer
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
        public Order MapOrder(Repositories.Models.Order order)
        {
            if(order == null)
            {
                return new Order();
            }

            return new Order
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
        public Address MapAddress(Repositories.Models.Address address)
        {
            if (address == null)
            {
                return new Address();
            }

            return new Address
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
        public OrderItem MapOrderItem(Repositories.Models.OrderItem item)
        {
            if (item == null)
            {
                return new OrderItem();
            }

            return new OrderItem
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
        public Product MapProduct(Repositories.Models.Product product)
        {
            if(product == null)
            {
                return new Product();
            }

            return new Product
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
        public IEnumerable<Customer> MapCustomers(IEnumerable<Repositories.Models.Customer> customers)
        {
            if (customers == null || !customers.Any())
            {
                return new List<Customer>
                {
                    new Customer()
                };
            }

            return customers.Select(customer => this.MapCustomer(customer));
        }

        /// <summary>
        /// Mapping order repository list into service
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>list</returns>
        public IEnumerable<Order> MapOrders(IEnumerable<Repositories.Models.Order> orders)
        {
            if (orders == null || !orders.Any())
            {
                return new List<Order>
                {
                    new Order()
                };
            }

            return orders.Select(order => this.MapOrder(order));
        }

        /// <summary>
        /// Mapping order item repository list into service
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns>list</returns>
        public IEnumerable<OrderItem> MapOrderItems(IEnumerable<Repositories.Models.OrderItem> orderItems)
        {
            if (orderItems == null || !orderItems.Any())
            {
                return new List<OrderItem>
                {
                    new OrderItem()
                };
            }

            return orderItems.Select(orderItem => this.MapOrderItem(orderItem));
        }

        /// <summary>
        /// Mapping product repository list into service
        /// </summary>
        /// <param name="products"></param>
        /// <returns>list</returns>
        public IEnumerable<Product> MapProducts(IEnumerable<Repositories.Models.Product> products)
        {
            if (products == null || !products.Any())
            {
                return new List<Product>
                {
                    new Product()
                };
            }

            return products.Select(product => this.MapProduct(product));
        }
    }
}
