var FormulaireAjout = React.createClass({
  render: function() {
    return (
      <div>
        <h1>Création d'un nouveau travail</h1>
        <form>
          <div className="fond">
            <p>
              <label htmlFor="nom">Nom du travail</label> : 
              <input type="text" name="nom" id="nom" required />
            </p>
            <p>
              <label htmlFor="dateRemise">Date de remise</label> :
              <input type="date" name="dateRemise" id="dateRemise" required />
            </p>
            <input type="submit" defaultValue="Envoyer" />
          </div> 
        </form>
      </div>
    );
  }
});