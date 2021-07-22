using System;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Business.Abstract;
using Xunit;

namespace Semsi
{
    public class UnitTest1
    {
        
        IProductService _productService;

        public UnitTest1(IProductService productService)
        {
            _productService = productService;
        }


        [Fact]
        public void Get_Products()
        {
            var products = _productService.GetAll().Data.Count;
            Assert.Equal(1,products);
        }

    }
}
