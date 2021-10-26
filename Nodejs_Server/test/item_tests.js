import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Item API', () => {

    var new_item ={
        "itemName" : "Knife",
        "price" : 0,
        "itemType" : "Accessory",
        "itemSource" : "Shop",
    }
    
    //Test GET Shop powerup
    describe('GET /item', () => {
        it("This should get all powerups in shop", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemSource: "Shop", itemType: "Powerup"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body[0]).to.have.property('itemName');
                expect(res.body[0]).to.have.property('price');
                expect(res.body[0]).to.have.property('spriteSource');
                expect(res.body[0]).to.have.property('itemSource').eq("Shop");
                expect(res.body[0]).to.have.property('itemType').eq("Powerup");
            done();
            })
        })
    })

    //Test GET leaderboard accessories
    describe('GET /item', () => {
        it("This should get all accessories on the leaderboard", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemSource: "Leaderboard", itemType: "Accessory"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                expect(res).to.be.json;
                expect(res.body[0]).to.have.property('itemName');
                expect(res.body[0]).to.have.property('price');
                expect(res.body[0]).to.have.property('spriteSource');
                expect(res.body[0]).to.have.property('itemSource').eq("Leaderboard");
                expect(res.body[0]).to.have.property('itemType').eq("Accessory");
            done();
            })
        })
    })

    //Test GET shop powerup with missing itemType param
    describe('GET /item', () => {
        it("This should get an error message for missing itemType param", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemSource: "Shop"})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing itemType param in request!");
            done();
            })
        })
    })

    //Test GET shop powerup with missing itemSource param
    describe('GET /item', () => {
        it("This should get an error message for missing itemSource param", (done) => {
            chai.request(server)
            .get("/items")
            .query({itemType: "Accessory"})
            .end((err, res) => {
                expect(res).to.have.status(400);
                res.text.should.be.eq("Missing itemSource param in request!");
            done();
            })
        })
    })

    //Test POST new item into the shop
    describe('POST /item', () => {
        it("This should add a user", (done) => {
            chai.request(server)
            .post("/items")
            .type("JSON")
            .send(new_item)
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("item added!");
            done();
            })
        })
    })

    // Test DELETE existing item
    describe('DELETE /item', () => {
        it("This should an existing item", (done) => {
            chai.request(server)
            .delete("/items")
            .query({name : "Knife"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("item is deleted!");
            done();
            })
        })
    })

    // Test DELETE item that does not exist
    describe('DELETE /item', () => {
        it("This should an existing item", (done) => {
            chai.request(server)
            .delete("/items")
            .query({name : "Knife"})
            .end((err, res) => {
                expect(res).to.have.status(200);
                res.text.should.be.eq("No such item!");
            done();
            })
        })
    })
})