namespace Service.ConcreteSpecifications
{
	public class AuthorSpecifications : Specifications<Author>
	{
		public AuthorSpecifications(Guid id)
			: base(a => a.Id == id)
		{
			AddInclude(a => a.Books);
		}
		public AuthorSpecifications(string name)
			: base(a => a.Name.Trim().ToUpper() == name.Trim().ToUpper())
		{
			AddInclude(a => a.Books);
		}
		public AuthorSpecifications()
			: base(null)
		{
			AddInclude(a => a.Books);
		}
	}
}
