using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.DataBase.Mappings
{
	public abstract class EntityCongurationMapper<T>: IEntityTypeConfiguration<T> where T : class
	{
		public abstract void Configure(EntityTypeBuilder<T> builder);
	}
}
