﻿using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Features.Songs;

namespace Touhou_Songs.Data
{
	public class Touhou_Songs_Context : DbContext
	{
		public Touhou_Songs_Context(DbContextOptions<Touhou_Songs_Context> options)
			: base(options)
		{
		}

		public DbSet<Song> Songs { get; set; } = default!;
	}
}
