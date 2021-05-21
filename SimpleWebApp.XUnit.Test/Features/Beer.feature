Feature: Beer
	Beer catalog functions

@test
Scenario: Register an unbranded beer
	Given I log in to the web app
  And I click in element 'Catalog Menu'
  And I click in element 'Beers Catalog'
  And I load the Page DOM Information 'Beer Catalog'
	When I click in element 'Add Beer Button'
  And I set element 'Brand Modal Form' with text ''
  And I set element 'Name Modal Form' with text 'Lager'
  And I set element 'Alcohol Modal Form' with text '1'
	Then I click in element 'Save Modal Button'
  And Assert if element 'Beer Validation' contains text ''Brand' must not be empty.'



