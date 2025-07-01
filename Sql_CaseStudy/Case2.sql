use AutoAccessDB

--1. Write a query to display customer list by the first name in descending order. 
select * from customers order by first_name desc

--2. Write a query to display the first name, last name, and city of the customers. It sorts the customer list by the city first and then by the first name.
select first_name,last_name,city from customers order by city,first_name

--3. Write a query to returns the top three most expensive products. 
select top 3 * from products order by list_price desc

--4. Write a query to finds the products whose list price is greater than 300 and model year is 2018. 
select * from products where list_price>300 and model_year=2018

--5. Write a query to finds products whose list price is greater than 3,000 or model year is 2018. Any product that meets one of these conditions is included in the result set. 
select * from products where list_price>3000 or model_year=2018

--6. Write a query to find the products whose list prices are between 1,899 and 1,999.99. 
select * from products where list_price between 1899 and 1999.99

--7.Write a query uses the IN operator to find products whose list price is 299.99 or 466.99 or 489.99. 
select * from products where list_price in (299.99,466.99,489.99)

--8. Write a query to the customers where the first character in the last name is the letter in the range A through C: 
select * from customers where last_name like '[ABC]%'

--9. Write a query using NOT LIKE operator to find customers where the first character in the first name is not the letter A:
select * from customers where first_name not like 'A%'

--10. Write a query to return the number of customers by state and city group state and city.
select state,city,count(*) from customers group by state,city 

--11. Write a query to return the number of orders placed by the customer group by customer id and year. 
select customer_id,count(*) from orders group by customer_id

--12. Write query to finds the maximum and minimum list group by category id. Then, it filters out the category which has the maximum list price greater than 4,000 or the minimum list price less than 500. 
select category_id, max(list_price) as max_price, min(list_price) as min_price from products group by category_id having max(list_price) <= 4000 and min(list_price) >= 500;
