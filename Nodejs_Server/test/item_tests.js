import chai from "chai";
import chaiHttp from "chai-http";
import {server} from "../app.js";
var expect = chai.expect;

//Assertion Style
chai.should();

chai.use(chaiHttp);

describe('Item API', () => {
    
    //Test GET item
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
            done();
            })
        })
    })
})