using System;
using System.Runtime.Serialization;
using Assignment3.ProblemDomain;

namespace Assignment3
{
    [DataContract]
    public class Node
    {
        [DataMember]
        public User user;

        [DataMember]
        public Node next;

        public Node(){}
        public Node(User value){user = value;}
        public Node(User value, Node nextNode){user = value; next = nextNode;}
    }
}