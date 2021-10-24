import {firestore} from "../model/db.js";

//add questions
const addQuestions = async(req, res) => {
    try {
        const qn = req.body;
        const questiondb = firestore.collection('questions');    
        questiondb.add(qn);
        res.send("question added");
    } catch (error) {
        res.status(400).send(error.message);
        res.send("error adding questions!");
    }
};

// Get questions by primary level
const getQuestions = async(req, res) => {
    try {
        const lvl = req.query.lvl;
        const questiondb = firestore.collection('questions');
        //todo: try to randomize qns to get    
        const snapshot = await questiondb.where('primaryLevel', '==', parseInt(lvl)).get();
        res.send(snapshot.docs.map(qn => qn.data()));

    } catch (error) {
        return res.status(400).send(error.message);
        //res.send("error getting questions!");
    }
};


export {getQuestions, addQuestions};