import {firestore} from "../model/db.js";

//add character
const addCharacter = async(req, res) => {
    try {
        if (req.body.characterName == null || req.body.characterName == ""){
            res.status(400).send("Missing character name!");
        }
        if (req.body.spriteSource == null || req.body.spriteSource == ""){
            res.status(400).send("Missing character sprite!");
        }
        if (req.body.characterDescription == null || req.body.characterDescription == ""){
            res.status(400).send("Missing character description!");
        }
        const character = req.body;
        const characterdb = firestore.collection('characters'); 
        characterdb.add(character).then(function(docRef) {
            characterdb.doc(docRef.id).set({"characterID": docRef.id}, { merge: true });
        });
        res.send("character added!");
    } catch (error) {
        res.status(400).send("error adding");
    }
};

// Get all characters
const getAllCharacters = async(req, res) => {
    try {
        const characterdb = firestore.collection('characters'); 
        const snapshot = await characterdb.get();
        res.send(snapshot.docs.map( ch => ch.data()));
    } catch (error) {
        res.status(400).send("error getting items!");
    }
};

// Get a character
const getCharacter = async(req, res) => {
    try {
        if (req.query.id == null || req.query.id == ""){
            res.status(400).send("Missing id param in request!");
        }
        const id = req.query.id;
        const characterdb = firestore.collection('characters'); 
        const curr = characterdb.where('characterID', '==', id);
        const snapshot = await curr.get();
        res.send(snapshot.docs.map( ch => ch.data())[0]);

    } catch (error) {
        res.status(400).send("error getting items!");
    }
};


//delete a character
const deleteCharacter = async(req, res) => {
    try {
        if (req.query.name == null || req.query.name == ""){
            res.status(400).send("Missing name param in request!");
        }
        const name = req.query.name;
        const characterdb = firestore.collection('characters'); 
        characterdb.where('characterName', '==', name).get().then(function(snapshot){
            if (!snapshot.empty) {
                snapshot.forEach(function(doc) {
                    doc.ref.delete();
                });
                return res.send("character is deleted!");
            } else {
                return res.status(400).send("No such character!");
            }
        });
    } catch (error) {
        return res.status(400).send("invalid name!");
    }
};




export {getCharacter, addCharacter, deleteCharacter, getAllCharacters};