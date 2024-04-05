using Assignment3.Utility;
using Assignment3.ProblemDomain;
using System.Transactions;
using System.Runtime.Serialization;

namespace  Assignment3
{
    [DataContract]
    public class SLL : ILinkedListADT
    {
        [DataMember]
        public Node ? head;

        public SLL(){ this.head = null; }

        public bool IsEmpty() // fine
        {
            if (this.head is null) return true;
            else return false;
        }

        public void Clear() // fine
        {
            this.head = null;
        }

        public void AddLast(User value) // fine
        {
            Node newNode = new Node(value);

            if (this.head is null)
            {
                this.head = newNode;
                return;
            }

            Node itr = this.head;
            while (!(itr.next is null)) itr = itr.next;
            itr.next = newNode;
        }

        public void AddFirst(User value) //fine
        {
            Node newNode = new Node(value);

            if (this.head is null)
            {
                this.head = newNode;
                return;
            }

            newNode.next = this.head;
            this.head = newNode;
        }

        public void Add(User value, int index) // fine
        {
            Node newNode = new Node(value);

            if (index < 0 || index >= this.Count()) throw new IndexOutOfRangeException("Index is out of range."); //outside possible range
            
            if (index == this.Count()) this.AddLast(value); //end of list
            else if (index == 0) this.AddFirst(value); // beginning of list
            else
            {
                int count = 0;
                Node itr = this.head;
                while (!(itr is null))
                {
                    if (count == index - 1) // node before index to add
                    {
                        newNode.next = itr.next;
                        itr.next = newNode;
                        return;
                    }
                    itr = itr.next;
                    count++;
                }
            }
        }
 
        public void Replace(User value, int index) // fine
        {
            Node newNode = new Node(value);
            
            if (index < 0 || index > this.Count() - 1) throw new IndexOutOfRangeException("Index is out of range."); // outside range

            if (index == 0) {this.RemoveFirst(); this.AddFirst(value);} // first value
            else if (index == this.Count() - 1) {this.RemoveLast(); this.AddLast(value);} //last value
            else 
            {
                Node itr = this.head;
                int count = 0;
                while (!(itr is null))
                {
                    if (count == index - 1) // node before index to replace
                    {
                        Node nextNode = itr.next.next;
                        newNode.next = nextNode;
                        itr.next = newNode;
                    }
                    itr = itr.next;
                    count++;
                }
            }
        }

        public int Count() // fine
        {
            int count = 0;
            Node itr = this.head;
            if (itr is null) return 0;
            while (!(itr is null)){
                count++;
                itr = itr.next;
            }
            return count;
        }

        public void RemoveFirst() // fine
        {
            if (this.head is null) throw new CannotRemoveException();

            this.head = this.head.next;
        }

        public void RemoveLast() // fine
        {
            if (this.head is null) throw new CannotRemoveException();

            Node itr = this.head;
            while (!(itr.next.next is null)) itr = itr.next;
            itr.next = null;
        }

        public void Remove(int index) // fine
        {
            if (index < 0 || index >= this.Count() - 1 ) throw new IndexOutOfRangeException("Index is out of range."); // outside list range

            if (index == 0) this.RemoveFirst(); // remove first
            else
            {
                int count = 0;
                Node itr = this.head;
                while (!(itr.next.next is null))
                {
                    if (count == index - 1)
                    {
                        itr.next = itr.next.next;
                        return;
                    } 
                    else 
                    {
                        itr = itr.next;
                        count++;
                    }
                }
                itr.next = null;
            }
        }

        public User GetValue(int index) // fine
        {
            // if index is out of range
            if (index < 0 || index >= this.Count()) throw new IndexOutOfRangeException("Index is out of range.");

            int count = 0;
            Node itr = this.head;
            while (count < index && !(itr.next is null)) // traverse list until at index
            {
                count++;
                itr = itr.next;
            }
            return itr.user;
        }

        public int IndexOf(User value) // fine
        {
            Node itr = this.head;
            int count = 0;
            while (!(itr is null)){
                if (itr.user == value) return count;
                else{
                    itr = itr.next;
                    count++;
                }
            }
            return -1;
        }

        public bool Contains(User value) // fine
        {
            Node itr = this.head;
            while (!(itr is null)){
                if (itr.user == value) return true;
                else itr = itr.next;
            }
            return false;
        }
    
        public void Display()
        {
            Node itr = this.head;
            Console.WriteLine();

            while (!(itr is null)){
                Console.Write(itr.user.Id + "-->");
                itr = itr.next;
            }
            Console.WriteLine();
        }

        public SLL Reverse() 
        {
            SLL newll = new SLL(); // empty ll
            Node itr = this.head;
            
            while (!(itr is null)){
                newll.AddFirst(itr.user); // add to beginning of new ll
                itr = itr.next;
            }
            return newll; 
        }
    }

    public class CannotRemoveException : Exception
    {
        public CannotRemoveException(): base("The list is empty.") {}
    }
}