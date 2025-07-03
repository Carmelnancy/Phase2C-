"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const ecommerce_service_1 = require("./services/ecommerce.service");
var service = new ecommerce_service_1.EcommerceService();
service.viewProducts();
service.addProduct("Laptop", 1);
service.removeProduct(1);
service.viewCart();
service.viewProducts();
