## tasks

##### 2) запрос, который выводит продукт и количество случаев, когда он был первой покупкой клиента
```sql
select productid ProductId_That_Sales_First, count(productid) Sales_Count
from (
select t.*, ts.productid from
(
select min(s.dtime) d, s.customerid cid from Sales s
group by s.customerid
) t
join Sales ts on ts.customerid = t.cid and ts.dtime = t.d
)
group by productid;
```
