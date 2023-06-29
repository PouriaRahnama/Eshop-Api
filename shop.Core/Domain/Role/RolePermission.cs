using shop.Core.Commons;
using shop.Core.Domain.Order;

namespace shop.Core.Domain.Role
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public Permission PermissionStatus { get; set; }
        public virtual Role Role { get; set; }
    }
    public enum Permission
    {
        //0
        AdminPanel,
        //1
        UserPanel,
        //2
        EditProfile,
        //3
        ChangePassword,
        //4
        CRUD_Banner,
        //5
        CRUD_Slider,
        //6
        CURD_User,
        //7
        CRUD_Product,
        //8
        Seller_Management,
        //9
        Order_Management,
        //10
        Role_Management,
        //11
        Comment_Management,
        //12
        Category_Management,
        //13
        Add_Inventory,
        //14
        Edit_Inventory,
        //15
        User_Management
    }
}
