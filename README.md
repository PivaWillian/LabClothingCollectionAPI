# LabClothingCollectionAPI = API created as the ending project of SENAI - LAB365 | Audaces DevInHouse's course module 2.

The default URL is https://localhost:7258
The API has three entities namely: "User", "Collection" and "Model".

The User's base endpoint is ["api/usuarios"]
The User has the following attributes: {id, Fullname, Gender, Email, BirthDate, DocNumber, PhoneNumber, UserType and Status}
The Get Method let's you fetch a collection of User and an Overload let's you search them by Status.
The Get["id"] let's you fetch a single user by it's id.
The Post let's you create a new User by giving it's attributes excluding the id that is given by the database.
The Put["id"] let's you change an User's values with the exception of the Email and Status.
The Put["id/status"] allows you to change a User's status.

The Collection's base endpoint is ["api/colecoes"]
The Collection has the following attributes: {id, Name, Owner, Brand, Budget, ReleaseDate, Season and Status}
The methods work in a similar fashion as the User's ones with the only difference being that collection has a 
Delete method["id"] that let's you delete a collection. As models are linked to Collection be aware that deleting a collection
deletes all models related to it.

The Model's base endpoint is ["api/modelos"]
The Model has the following attributes: {Id, Name, CollectionId, Models*, Layout} the Models attribute refers to which
kind of clothe's piece the model refers to.
The methods work in the same way as the Collections one, even the delete one.
