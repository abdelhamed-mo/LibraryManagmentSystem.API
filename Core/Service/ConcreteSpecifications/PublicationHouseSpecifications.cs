namespace Service.ConcreteSpecifications
{
	public class PublicationHouseSpecifications : Specifications<PublishingHouse>
	{
        public PublicationHouseSpecifications(Guid id)
			: base(h => h.Id == id)
		{
			AddInclude(h => h.Books);
		}
		public PublicationHouseSpecifications(string name)
			: base(h => h.Name.Trim().ToUpper() == name.Trim().ToUpper())
		{
			AddInclude(h => h.Books);
		}
        public PublicationHouseSpecifications()
			: base(null)
		{
			AddInclude(h => h.Books);
		}
	}
}
