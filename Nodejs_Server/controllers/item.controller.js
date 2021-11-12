import {firestore} from "../model/db.js";

//add items
const addItems = async(req, res) => {
    try {
        const items = req.body;
        const itemsdb = firestore.collection('items'); 
        itemsdb.add(items).then(function(docRef) {
            itemsdb.doc(docRef.id).set({"id": docRef.id}, { merge: true });
        });
        res.send("item added!");
    } catch (error) {
        res.status(400).send("error in adding item");
    }
};
// Get all items in shop
const getItems = async(req, res) => {
    try {
        if (req.query.itemSource == null || req.query.itemSource == ""){
            res.status(400).send("Missing itemSource param in request!");
        }
        if (req.query.itemType == null || req.query.itemType == ""){
            res.status(400).send("Missing itemType param in request!");
        }
        const source = req.query.itemSource;
        const type = req.query.itemType;
        var itemsdb = firestore.collection('items');
        itemsdb = itemsdb.where('itemSource', '==', source);
        itemsdb = itemsdb.where("itemType", "==", type);
        const snapshot = await itemsdb.get();
        res.send(snapshot.docs.map( item=> item.data()));

    } catch (error) {
        res.status(400).send("error getting items!");
    }
};

const deleteItem = async(req, res) => {
    try {
        const name = req.query.name;
        var itemsdb = firestore.collection('items');
        itemsdb.where('itemName', '==', name).get().then(function(snapshot){
            if (!snapshot.empty) {
                snapshot.forEach(function(doc) {
                    doc.ref.delete();
                });
                return res.send("item is deleted!");
            } else {
                return res.status(400).send("No such item!");
            }
        });
    } catch (error) {
        return res.status(400).send("invalid name!");
    }
};

export {getItems, addItems, deleteItem};