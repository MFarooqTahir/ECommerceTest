﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ECommerceText.CartApi.DbContexts;
using ECommerceText.CartApi.DbContexts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ECommerceText.CartApi.DbContexts.Configurations
{
    public partial class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.UserId).HasColumnName("UserID");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Cart> entity);
    }
}