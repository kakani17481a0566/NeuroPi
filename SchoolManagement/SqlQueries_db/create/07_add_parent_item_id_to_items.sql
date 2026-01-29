ALTER TABLE items
ADD COLUMN parent_item_id integer NULL;

ALTER TABLE items
ADD CONSTRAINT fk_items_parent 
FOREIGN KEY (parent_item_id) 
REFERENCES items (id);
