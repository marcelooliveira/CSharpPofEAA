Implicit Lock

by David Rice

Allows framework or layer supertype code to acquire offline locks.

For a full description see P of EAA page 449

![File](file.png) 

The key to any locking scheme is that there are no gaps in its use. Forgetting to write a single line of code that acquires a lock can render an entire offline lock-ing scheme useless. Failing to retrieve a read lock where other transactions use write locks means you might not get up-to-date session data; failing to use a version count properly can result in unknowingly writing over someone's changes. Generally, if an item might be locked anywhere it must be locked everywhere. Ignoring its application's locking strategy allows a business trans-action to create inconsistent data. Not releasing locks won't corrupt your record data, but it will eventually bring productivity to a halt. Because offline concurrency management is difficult to test, such errors might go undetected by all of your test suites.

One solution is to not allow developers to make such a mistake. Locking tasks that cannot be overlooked should be handled not explicitly by developers but implicitly by the application. The fact that most enterprise applications make use of some combination of framework, Layer Supertypes (475), and code generation provides us with ample opportunity to facilitate Implicit Lock.