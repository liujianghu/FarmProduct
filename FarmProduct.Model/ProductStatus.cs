
namespace FarmProduct.Model
{
    public enum ProductStatus : int
    {
        IsDeleted = -1,

        /// <summary>
        /// 原始农产品
        /// </summary>
        Procreative = 1,

        /// <summary>
        /// 已销售的农村产品
        /// </summary>
        SoldAgriculturalProduct = 2,

        /// <summary>
        /// 批发产品：可批发，可分割
        /// </summary>
        WholeSale = 3,

        /// <summary>
        /// 已分割的批发产品
        /// </summary>
        CutWholeSaleProduct = 4,

        /// <summary>
        /// 已批发
        /// </summary>
        WholeSold = 5,

        /// <summary>
        /// 零售状态： 可分割，可销售
        /// </summary>
        Retail = 6,

        /// <summary>
        /// 已分割的零售产品
        /// </summary>
        CutRetailProduct = 7,

        /// <summary>
        /// 已销售
        /// </summary>
        Retailed = 8,

        /// <summary>
        /// 可批发产品
        /// </summary>
        CanWholeSale = 9,

        /// <summary>
        /// 可零售产品
        /// </summary>
        CanRetail = 10

    }
}
