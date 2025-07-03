"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EcommerceService = void 0;
const category_enum_1 = require("../models/category.enum");
class EcommerceService {
    constructor() {
        this.products = [];
        this.cart = [];
        this.products = [
            { id: 1, name: "Laptop", category: category_enum_1.Category.Electronics, price: 45000, stock: 3 },
            { id: 2, name: "Jeans", category: category_enum_1.Category.Clothing, price: 1500, stock: 10 },
            { id: 3, name: "Rice Bag", category: category_enum_1.Category.Grocery, price: 700, stock: 5 }
        ];
        this.cart = [];
    }
    viewCart() {
        console.log("Cart Summary:");
        let total = 0;
        for (const item of this.cart) {
            const subtotal = item.product.price * item.quantity;
            total += subtotal;
            console.log(`${item.product.name} - ${item.product.price} x ${item.quantity}`);
        }
        const discount = this.getDiscount(total);
        const discountedTotal = total - total * discount;
        console.log(`Total: ${total}`);
        console.log(`Discounted Total: ${discountedTotal}`);
    }
    getDiscount(total) {
        if (total >= 10000)
            return 0.15;
        if (total >= 5000)
            return 0.10;
        return 0;
    }
    removeProduct(productId) {
        const index = this.cart.findIndex(c => c.product.id === productId);
        if (index === -1) {
            console.log("Product not in cart.");
            return;
        }
        const item = this.cart[index];
        item.product.stock += item.quantity;
        console.log(`${item.quantity} x ${item.product.name} removed from cart.`);
        this.cart.splice(index, 1);
    }
    addProduct(productname, quantity) {
        let pro = this.products.find(p => p.name === productname);
        if (!pro) {
            console.log("Product not available");
            return;
        }
        if (pro.stock < quantity) {
            console.log("Product outof stock");
            return;
        }
        let cartItem = this.cart.find(c => c.product.name === productname);
        if (cartItem) {
            cartItem.quantity += quantity;
        }
        else {
            this.cart.push({ product: pro, quantity });
        }
        pro.stock -= quantity;
        console.log(`${quantity} x ${pro.name} added to cart.`);
    }
    viewProducts() {
        console.log("Available Products:");
        for (const pro of this.products) {
            console.log(`${pro.name} | ${pro.price} | In stock: ${pro.stock} | Category: ${pro.category}`);
        }
    }
}
exports.EcommerceService = EcommerceService;
