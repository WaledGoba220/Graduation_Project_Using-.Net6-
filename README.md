# Graduation Project (GOBA)

[![Semantic description of image](/Graduation_Project/wwwroot/images/about.jpg "Hello World")

## Abstract
 Our website aims to provide several customized services to the user, whether the user is a patient or a doctor, as the
 site has several features and advantages.
 <br>
 **There are four main servants:**
1. **Diseases Detection:** Some serious lung diseases that are difficult to detect with the naked eye are detected, such as pulmonary fibrosis, tuberculosis and lung cancer. The doctor uploads the x-ray image to the site, and through artificial intelligence and machine learning, it is recognized if the patient has the disease, is healthy, or is exposed to the disease at a certain percentage.
2. **Measuring box:** Through the measurement box, the heart rate, blood oxygen level, and temperature are measured through the finger of the hand, and these percentages are recorded on the site, and they are shown directly on its mobile application.
3. **Medical Advices:** Through medical advices, the doctor can add medical advice and users interact with it, and they can add a comment or add this advice to favorites.
4. **Lung Transplant:** It is a form through which all personal data and all requirements are added that prove that the patient needs a lung transplant, and the validity of this data is detected by artificial intelligence and the authority concerned with lung transplantation to facilitate this process.

## System Architecture
### Onion Architecture
> The Onion architecture is also commonly known as the “Clean architecture” or “Ports and adapters”. These architectural approaches are just variations of the same theme.

### What is the Onion Architecture?
> The Onion architecture is a form of layered architecture and we can visualize these layers as concentric circles. Hence the name Onion architecture. The Onion architecture was first introduced by Jeffrey Palermo, to overcome the issues of the traditional N-layered architecture approach.

There are multiple ways that we can split the onion, but we are going to choose the following approach where we are going to split the architecture into **4 layers:**
1. Domain Layer
2. Service Layer
3. Infrastructure Layer
4. Presentation Layer

Conceptually, we can consider that the Infrastructure and Presentation layers are on the same level of the hierarchy.
Now, let us go ahead and look at each layer with more detail to see why we are introducing it and what we are going to create inside of that layer:
> Image

### Advantages of the Onion Architecture
> Let us take a look at what are the advantages of Onion architecture, and why we would want to implement it in our projects.
> <br>
> All of the layers interact with each other strictly through the interfaces defined in the layers below. The flow of dependencies is towards the core of the Onion. We will explain why this is important in the next section.
> <br>
> Using dependency inversion throughout the project, depending on abstractions (interfaces) and not the implementations, allows us to switch out the implementation at runtime transparently.
> <br>
> We are depending on abstractions at compile-time, which gives us strict contracts to work with, and we are being provided with the implementation at runtime.
> <br>
> Testability is very high with the Onion architecture because everything depends on abstractions.
> <br>
> The abstractions can be easily mocked with a mocking library such as Moq.
> <br>
> To learn more about unit testing your projects in ASP.NET Core check out this article Testing MVC Controllers in ASP.NET Core.
> <br>
> We can write business logic without concern about any of the implementation details. If we need anything from an external system or service, we can just create an interface for it and consume it. We do not have to worry about how it will be implemented. The higher layers of the Onion will take care of implementing that interface transparently.
- **Flow of Dependencies**
> The main idea behind the Onion architecture is the flow of dependencies, or rather how the layers interact with each other. The deeper the layer resides inside the Onion, the fewer dependencies it has.
> The Domain layer does not have any direct dependencies on the outside layers. It is isolated, in a way, from the outside world. The outer layers are all allowed to reference the layers that are directly below them in the hierarchy.
> We can conclude that all the dependencies in the Onion architecture flow inwards. But we should ask ourselves, why is this important?
> The flow of dependencies dictates what a certain layer in the Onion architecture can do. Because it depends on the layers below it in the hierarchy, it can only call the methods that are exposed by the lower layers.
> We can use lower layers of the Onion architecture to define contracts or interfaces. The outer layers of the architecture implement these interfaces. This means that in the Domain layer, we are not concerning ourselves with infrastructure details such as the database or external services.
> Using this approach, we can encapsulate all of the rich business logic in the Domain and Service layers without ever having to know any implementation details. In the Service layer, we are going to depend only on the interfaces that are defined by the layer below, which is the Domain layer.
> Enough theory, let us see some code. We have already prepared a working project for you and we’re going to be looking at each of the projects in the solution, and talking about how they fit into the Onion architecture.

### Solution Structure
> As we can see, it consists of the Web project, which is our ASP.NET Core application, and six class libraries. The Domain project will hold the Domain layer implementation. The Services and Services.
> <br>
> Abstractions are going to be our Service layer implementation. The Persistence project will be our Infrastructure layer, and the Presentation project will be the Presentation layer implementation.

### **1. Domain Layer**
> The Domain layer is at the core of the Onion architecture. In this layer, we are typically going to define the core aspects of our domain:
> - Entities
> - Repository interfaces
> - Exceptions
> - Domain services

### **2. Service Layer**
> The Service layer sits right above the Domain layer, which means that it has a reference to the Domain layer.
> <br>
> The Service layer is split into two projects, Services.Abstractions and Services.
> <br>
> In the Services.Abstractions project you can find the definitions for the service interfaces that are going to encapsulate the main business logic. Also, we are using the Contracts project to define the Data Transfer Objects (DTO) that we are going to consume with the service interfaces.
> 
**What is the motivation for splitting the Service layer?**
> - Why are we going through so much trouble to split our service interfaces and implementations into two separate projects?
> - As you can see, we mark the service implementations with the internal keyword, which means they will not be publicly available outside of the Services project. > - On the other hand, the service interfaces are public.
> - Do you remember what we said about the flow of dependencies?
> - With this approach, we are being very explicit about what the higher layers of the Onion can and can not do. It is easy to miss here that the 
> - Services.Abstractions project does not have a reference to the Domain project.
> - This means that when a higher layer references the Services.Abstractions project it will only be able to call methods that are exposed by this project.
> - We are going to see why this is very useful later on when we get to the Presentation layer.

### **3. Infrastructure Layer**
> The Infrastructure layer should be concerned with encapsulating anything related to external systems or services that our application is interacting with.
> <br>
> These external services can be:
> - Database
> - Identity provider
> - Messaging queue
> - Email service

### **4. Presentation Layer**
> The purpose of the Presentation layer is to represent the entry point to our system so that consumers can interact with the data.
> <br>
> We can implement this layer in many ways, for example creating a REST API, gRPC, etc.
> <br>
> We are using a Web API built with ASP.NET Core to create a set of RESTful API endpoints for modifying the domain entities and allowing consumers to get back the data.
> <br>
> However, we are going to do something different from what you are normally used to when creating Web APIs. By convention, the controllers are defined in the Controllers folder inside of the Web application.
> <br>
> Why is this a problem? Because ASP.NET Core uses Dependency Injection everywhere, we need to have a reference to all of the projects in the solution from the Web application project. This allows us to configure our services inside of the Startup class.
> <br>
> While this is exactly what we want to do, it introduces a big design flaw. What is preventing our controllers from injecting anything they want inside the constructor? Nothing!




