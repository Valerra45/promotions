namespace Domain.Promotions
{
    public class SendPromotion : BaseEntity
    {
        public virtual Promotion? Promotion { get; set; }

        public virtual Partner? Partner { get; set; }

        public virtual Manager? Manager { get; set; }
    }
}
