"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EcommerceService = void 0;
var category_enum_1 = require("../models/category.enum");
var EcommerceService = /** @class */ (function () {
    function EcommerceService() {
        this.products = [];
        this.products = [
            { id: 1, name: "Laptop", category: category_enum_1.Category.Electronics, price: 45000, stock: 3 },
            { id: 2, name: "Jeans", category: category_enum_1.Category.Clothing, price: 1500, stock: 10 },
            { id: 3, name: "Rice Bag", category: category_enum_1.Category.Grocery, price: 700, stock: 5 }
        ];
    }
    EcommerceService.prototype.viewCart = function () {
        console.log("Cart Summary:");
        var total = 0;
        for (var _i = 0, _a = this.cart; _i < _a.length; _i++) {
            var item = _a[_i];
            var subtotal = item.product.price * item.quantity;
            total += subtotal;
            console.log("".concat(item.product.name, " - ").concat(item.product.price, " x ").concat(item.quantity));
        }
        var discount = this.getDiscount(total);
        var discountedTotal = total - total * discount;
        console.log("Total: ".concat(total));
        console.log("Discounted Total: ".concat(discountedTotal));
    };
    EcommerceService.prototype.getDiscount = function (total) {
        if (total >= 10000)
            return 0.15;
        if (total >= 5000)
            return 0.10;
        return 0;
    };
    EcommerceService.prototype.removeProduct = function (productId) {
        var index = this.cart.findIndex(function (c) { return c.product.id === productId; });
        if (index === -1) {
            console.log("Product not in cart.");
            return;
        }
        var item = this.cart[index];
        item.product.stock += item.quantity;
        console.log("".concat(item.quantity, " x ").concat(item.product.name, " removed from cart."));
        this.cart.splice(index, 1);
    };
    EcommerceService.prototype.addProduct = function (productname, quantity) {
        var pro = this.products.find(function (p) { return p.name === productname; });
        if (!pro) {
            console.log("Product not available");
            return;
        }
        if (pro.stock < quantity) {
            console.log("Product outof stock");
            return;
        }
        var cartItem = this.cart.find(function (c) { return c.product.name === productname; });
        if (cartItem) {
            cartItem.quantity += quantity;
        }
        else {
            this.cart.push({ pro: pro, quantity: quantity });
        }
    };
    EcommerceService.prototype.viewProducts = function () {
        console.log("Available Products:");
        for (var _i = 0, _a = this.products; _i < _a.length; _i++) {
            var pro = _a[_i];
            console.log("".concat(pro.name, " | ").concat(pro.price, " | In stock: ").concat(pro.stock, " | Category: ").concat(pro.category));
        }
    };
    return EcommerceService;
}());
exports.EcommerceService = EcommerceService;
