import { EcommerceService } from "./services/ecommerce.service";

var service=new EcommerceService();

service.viewProducts();

service.addProduct("Laptop",1);

service.removeProduct(1);

service.viewCart();

service.viewProducts();


