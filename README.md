# Requirements Specification

## Use Case: Customer's admin user deletes licensed user

### Scenario: Customer uses Automatic Licensing Mode

```
Given I am a customer's admin user
When I delete licensed user
Then the user should be deleted
  And the quantity of the affected subscriptions
  should be aligned with the number of assigned
  licenses
```

### Scenario: Customer uses Manual Licensing Mode

```
Given I am a customer's admin user
When I delete licensed user
Then the user should be deleted
  And the quantity of the affected subscriptions
  should be unaffected
```