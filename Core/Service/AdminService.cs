using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.BookModule;
using Microsoft.AspNetCore.Http;
using Shared.AdminModels;
using Gender = Domain.Entities.BookModule.Gender;

namespace Service
{
	public class AdminService(IUnitOfWork unitOfWork, IMapper mapper) : IAdminService
	{
		#region Books

		public async Task<string> UploadBook(UploadBookDto book)
		{
			// search for the book
			var b = await unitOfWork.GetRepository<Book, Guid>().GetAllAsync(new BookSpecifications(new BooksParams() { Search = book.Title }));
			if (b != null) return $"({book.Title}) This Book Is Already Uploaded.✔️";

			if (book.Pdf == null || book.Pdf.Length == 0)
				return "PDF file is required.📂❌";

			if (book.Picture == null || book.Picture.Length == 0)
				return "Cover image is required.📂❌";

			// get folder path for pdf
			var PdfFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\PdfFiles");
			if (!Directory.Exists(PdfFolder))
				Directory.CreateDirectory(PdfFolder);


			// save PDF 
			var pdfName = $"{Guid.NewGuid()}-{Path.GetFileName(book.Pdf.FileName)}";
			var pdfPath = Path.Combine(PdfFolder, pdfName);
			using (var stream = new FileStream(pdfPath, FileMode.Create))
			{
				await book.Pdf.CopyToAsync(stream);
			}
			// get folder path for picture/books
			var PictureFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Books");
			if (!Directory.Exists(PictureFolder))
				Directory.CreateDirectory(PictureFolder);

			// save Picture 
			var imgName = $"{Guid.NewGuid()}-{Path.GetFileName(book.Picture.FileName)}";
			var imgPath = Path.Combine(PictureFolder, imgName);
			using (var stream = new FileStream(imgPath, FileMode.Create))
			{
				await book.Picture.CopyToAsync(stream);
			}

			var Book = new Book
			{
				Id = Guid.NewGuid(),
				Title = book.Title.Trim(),
				Description = book.Description.Trim(),
				PublicationDate = book.PublicationDate,
				Category = book.Category.Trim(),

				PdfUrl = pdfName,
				PictureUrl = imgName,

				//Price = book.SellingPrice,
				//RentPrice = book.RentPrice,

				//QuantityForSale = book.QuantityForSale,
				//QuantityForRent = book.QuantityForRent,

				PublishingHouseId = book.PublishingHouseId,
				AuthorId = book.AuthorId,
			};

			await unitOfWork.GetRepository<Book, Guid>().AddAsync(Book);
			await unitOfWork.SaveChangesAsync();

			return "Book Uploaded Successfully✔️";
		}
		public async Task<string> UpdateBook(UpdateBookDto book)
		{
			var Book = await unitOfWork.GetRepository<Book, Guid>().GetAsync(book.Id);
			if (Book == null) throw new BookNotFoundException(book.Id);

			if (book.Pdf != null && book.Pdf.Length > 0)
			{
				// delete old pdf
				var oldPdf = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\PdfFiles", Book.PdfUrl);
				if (File.Exists(oldPdf))
					File.Delete(oldPdf);

				// get folder path for pdf
				var PdfFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\PdfFiles");
				if (!Directory.Exists(PdfFolder))
					Directory.CreateDirectory(PdfFolder);

				// save PDF 
				var pdfName = $"{Guid.NewGuid()}-{Path.GetFileName(book.Pdf.FileName)}";
				var pdfPath = Path.Combine(PdfFolder, pdfName);
				using (var stream = new FileStream(pdfPath, FileMode.Create))
				{
					await book.Pdf.CopyToAsync(stream);
				}
				Book.PdfUrl = pdfName;
			}

			if (book.Picture != null && book.Picture.Length > 0)
			{
				// delete old picture
				var oldPicture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Books", Book.PictureUrl);
				if (File.Exists(oldPicture))
					File.Delete(oldPicture);

				// get folder path for picture/books
				var PictureFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Books");
				if (!Directory.Exists(PictureFolder))
					Directory.CreateDirectory(PictureFolder);

				// save Picture 
				var imgName = $"{Guid.NewGuid()}-{Path.GetFileName(book.Picture.FileName)}";
				var imgPath = Path.Combine(PictureFolder, imgName);
				using (var stream = new FileStream(imgPath, FileMode.Create))
				{
					await book.Picture.CopyToAsync(stream);
				}
				Book.PictureUrl = imgName;
			}


			Book.Title = book.Title.Trim();
			Book.Description = book.Description.Trim();
			Book.PublicationDate = book.PublicationDate;
			Book.Category = book.Category.Trim();

			//Book.SellingPrice = book.SellingPrice;
			//Book.RentPrice = book.RentPrice;

			//Book.QuantityForSale = book.QuantityForSale;
			//Book.QuantityForRent = book.QuantityForRent;

			Book.PublishingHouseId = book.PublishingHouseId;
			Book.AuthorId = book.AuthorId;

			unitOfWork.GetRepository<Book, Guid>().Update(Book);
			await unitOfWork.SaveChangesAsync();

			return "Book updated successfully.✔️";
		}
		public async Task<string> DeleteBook(Guid id)
		{
			var Book = await unitOfWork.GetRepository<Book, Guid>().GetAsync(id);
			if (Book == null) throw new BookNotFoundException(id);

			// delete pdf & picture
			var Pdf = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\PdfFiles", Book.PdfUrl);
			if (File.Exists(Pdf))
				File.Delete(Pdf);

			var Picture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Books", Book.PictureUrl);
			if (File.Exists(Picture))
				File.Delete(Picture);

			unitOfWork.GetRepository<Book, Guid>().Delete(Book);
			await unitOfWork.SaveChangesAsync();
			return $"{Book.Title} deleted successfully.✔️";
		}

		public async Task<FullBookDto> GetBookById(Guid id)
		{
			var book = await unitOfWork
					.GetRepository<Book, Guid>()
					.GetAsync(new BookSpecifications(id));

			return book is null
				? throw new BookNotFoundException(id)
				: mapper.Map<FullBookDto>(book);
		}
		public async Task<PaginatedResultDto<FullBookDto>> GetAllBooks(BooksParams _params)
		{
			var books = mapper.Map<IEnumerable<FullBookDto>>(await unitOfWork
					.GetRepository<Book, Guid>().GetAllAsync(new BookSpecifications(_params)));

			var totalCount = await unitOfWork
					.GetRepository<Book, Guid>().CountAsync(new BooksPaginatedResultSpecifications(_params));

			return new PaginatedResultDto<FullBookDto>(
				books.Count(),
				_params.PageIndex,
				totalCount,
				books);
		}

		#endregion

		#region Authors
		public async Task<string> AddAuthor(AddAuthorDto author)
		{
			var a = await unitOfWork.GetRepository<Author, Guid>().GetAsync(new AuthorSpecifications(author.Name));
			if (a != null) return $"({author.Name}) This Author Is Already Added.✔️";

			if (author.Picture == null || author.Picture.Length == 0)
				return "Picture is required.📂❌";
			// get folder path for picture/authors
			var PictureFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Authors");
			if (!Directory.Exists(PictureFolder))
				Directory.CreateDirectory(PictureFolder);

			// save Picture 
			var imgName = $"{Guid.NewGuid()}-{Path.GetFileName(author.Picture.FileName)}";
			var imgPath = Path.Combine(PictureFolder, imgName);
			using (var stream = new FileStream(imgPath, FileMode.Create))
			{
				await author.Picture.CopyToAsync(stream);
			}

			var Author = new Author
			{
				Id = Guid.NewGuid(),
				Name = author.Name.Trim(),
				Bio = author.Bio.Trim(),
				BirthDate = author.BirthDate,
				Gender = (Gender)author.Gender,
				Nationality = author.Nationality.Trim(),
				PicturUrl = imgName
			};

			await unitOfWork.GetRepository<Author, Guid>().AddAsync(Author);
			await unitOfWork.SaveChangesAsync();

			return "Author Added Successfully.✔️";
		}

		public async Task<string> UpdateAuthor(UpdateAuthorDto author)
		{
			var Author = await unitOfWork.GetRepository<Author, Guid>().GetAsync(author.Id);
			if (Author == null) throw new AuthorNotFoundException(author.Id);

			if (author.Picture != null && author.Picture.Length > 0)
			{
				// delete old picture
				var oldPicture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Authors", Author.PicturUrl);
				if (File.Exists(oldPicture))
					File.Delete(oldPicture);

				// get folder path for pictures/authors
				var PictureFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Authors");
				if (!Directory.Exists(PictureFolder))
					Directory.CreateDirectory(PictureFolder);

				// save Picture 
				var imgName = $"{Guid.NewGuid()}-{Path.GetFileName(author.Picture.FileName)}";
				var imgPath = Path.Combine(PictureFolder, imgName);
				using (var stream = new FileStream(imgPath, FileMode.Create))
				{
					await author.Picture.CopyToAsync(stream);
				}
				Author.PicturUrl = imgName;
			}

			Author.Name = author.Name.Trim();
			Author.Bio = author.Bio.Trim();
			Author.BirthDate = author.BirthDate;
			Author.Gender = (Gender)author.Gender;
			Author.Nationality = author.Nationality.Trim();

			unitOfWork.GetRepository<Author, Guid>().Update(Author);
			await unitOfWork.SaveChangesAsync();

			return "Author Updated Successfully.✔️";
		}

		public async Task<string> DeleteAuthor(Guid id)
		{
			var Author = await unitOfWork.GetRepository<Author, Guid>().GetAsync(id);
			if (Author == null) throw new AuthorNotFoundException(id);

			// delete picture
			var picture = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Pictures\\Authors", Author.PicturUrl);
			if (File.Exists(picture))
				File.Delete(picture);

			unitOfWork.GetRepository<Author, Guid>().Delete(Author);
			await unitOfWork.SaveChangesAsync();

			return "Author Deleted Successfully.✔️";
		}

		public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
		=> mapper.Map<IEnumerable<AuthorDto>>(await unitOfWork
			.GetRepository<Author, Guid>().GetAllAsync());
		public async Task<AuthorDto> GetAuthorByIdAsync(Guid id)
		{
			var author = await unitOfWork.GetRepository<Author, Guid>().GetAsync(id);
			return author is null
				? throw new AuthorNotFoundException(id)
				: mapper.Map<AuthorDto>(author);
		}

		#endregion

		#region publishingHouse
		public async Task<IEnumerable<PublishingHouseDto>> GetAllPublishingHousesAsync()
		=> mapper.Map<IEnumerable<PublishingHouseDto>>(await unitOfWork
			.GetRepository<PublishingHouse, Guid>().GetAllAsync(new PublicationHouseSpecifications()));
		public async Task<PublishingHouseDto> GetPublishingHouseByIdAsync(Guid id)
		{
			var publishingHouse = await unitOfWork.GetRepository<PublishingHouse, Guid>().GetAsync(id);
			return publishingHouse is null
				? throw new PublishingHouseNotFoundException(id)
				: mapper.Map<PublishingHouseDto>(publishingHouse);
		}

		public async Task<string> AddPublishingHouse(AddPublishingHouseDto publishingHouse)
		{
			var house = await unitOfWork.GetRepository<PublishingHouse, Guid>().GetAsync(new PublicationHouseSpecifications(publishingHouse.Name));
			if (house is not null) return "Publishing House is already added.✔️";

			var House = new PublishingHouse()
			{
				Id = Guid.NewGuid(),
				Name = publishingHouse.Name.Trim(),
				Location = publishingHouse.Location
			};

			await unitOfWork.GetRepository<PublishingHouse,Guid>().AddAsync(House);
			await unitOfWork.SaveChangesAsync();

			return "Publishing House Added Successfully.✔️";
		}

		public async Task<string> UpdatePublishingHouse(UpdatePublishingHouseDto publishingHouse)
		{
			var House = await unitOfWork.GetRepository<PublishingHouse, Guid>().GetAsync(new PublicationHouseSpecifications(publishingHouse.Id));
			if (House is null) throw new PublishingHouseNotFoundException(publishingHouse!.Id);

			House.Name = publishingHouse.Name.Trim();
			House.Location = publishingHouse.Location;
			

			unitOfWork.GetRepository<PublishingHouse, Guid>().Update(House);
			await unitOfWork.SaveChangesAsync();

			return "Publishing House Updated Successfully.✔️";
		}

		public async Task<string> DeletePublishingHouse(Guid id)
		{
			var House = await unitOfWork.GetRepository<PublishingHouse, Guid>().GetAsync(new PublicationHouseSpecifications(id));
			if (House is null) throw new PublishingHouseNotFoundException(id);

			unitOfWork.GetRepository<PublishingHouse, Guid>().Delete(House);
			await unitOfWork.SaveChangesAsync();

			return "Publishing House Deleted Successfully.✔️";
        }



		#endregion
	}
}
