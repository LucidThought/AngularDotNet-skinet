Angular Obeservables
- regularly long operations have to run in Promises (or as async await) which succeed or fail
- Angular provides Observables, which allow us to Cancel those requests easily, as well as subscribe(), map(), or filter() the data returned. If error occur they can be caught in the same way that promises are tried and caught
