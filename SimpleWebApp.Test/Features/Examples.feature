Feature: Examples
  Optional description of feature

  @register
  Scenario: Assert contain text
    Given I am in App main site
    Then I load the DOM Information 'SimpleWebApp_RegisterPage.json'
    And I click in element 'Email'
    And I set element 'Email' with text 'luisfrr27@gmail.com'
    And I click in element 'Email'
    And I set element 'Password' with text 'cotemar'
    And I click in element 'Confirm Password'
    And I set element 'Confirm Password' with text 'cotemar'
    And I click in element 'Register Button'
    Then Assert if element 'Register Form Error' contains text 'Username 'luisfrr27@gmail.com' is already taken.'
